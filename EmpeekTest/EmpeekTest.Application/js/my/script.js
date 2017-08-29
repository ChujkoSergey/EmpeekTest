var app = angular.module('mainApp', []);
app.controller('mainController', function ($scope, $http) {

    function RefreshTable() {
        $http.get("api/main")
            .then(function (response) {
                $scope.items = response.data;
                var temp = response.data.length % 3;
                if (temp < 2) {
                    $scope.count = new Array(Math.round(response.data.length / 3) + 1);
                }
                else {
                    $scope.count = new Array(Math.round(response.data.length / 3));
                }
                var length = $scope.count.length;
                for (var i = 1; i <= length; i++) {
                    $scope.count.push(i);
                }

            });
    }

    RefreshTable();

    $scope.AddNewItem = function () {
        $http.post("api/main/add", JSON.stringify({Name: $scope.newItemName, Type: $scope.newItemType})).then(function (response) {
            var temp = response.data;
            if (temp.ResultCode != 1) {
                alert(temp.Message);
            }
            else {
                RefreshTable();
            }
        });
    }


});

var statApp = angular.module('statApp', []);
statApp.controller('statController', function ($scope, $http) {
    $http.get("api/stat")
        .then(function (response) {
            $scope.types = response.data;
        });
});