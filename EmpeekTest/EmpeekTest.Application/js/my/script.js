var app = angular.module('mainApp', []);
app.controller('mainController', function ($scope, $http) {
    $scope.itemsCount = 4;
    $scope.currentPage = 1;
    $scope.showEdit = false;
    function RefreshTable(page, count) {
        $http.post("api/main", JSON.stringify({ Page: page, Count: count}))
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

    RefreshTable($scope.currentPage, $scope.itemsCount);

    $scope.AddNewItem = function () {
        $http.post("api/main/add", JSON.stringify({ Name: $scope.newItemName, Type: $scope.newItemType }))
            .then(function (response) {
                var temp = response.data;
                if (temp.ResultCode != 1) {
                    alert(temp.Message);
                }
                else {
                    RefreshTable($scope.currentPage, $scope.itemsCount);
                }
            });
    };

    $scope.DeleteItem = function (id) {
        $http.post("api/main/delete", JSON.stringify({ Id: id }))
            .then(function (response) {
                var temp = response.data;
                if (temp.ResultCode != 1) {
                    alert(temp.Message);
                }
                else {
                    RefreshTable($scope.currentPage, $scope.itemsCount);
                }
            });
    };

    $scope.EditItem = function (id) {
        var temp = $scope.items.find(function (item) {return item.Id == id;});
        $scope.editItemName = temp.Name;
        $scope.editItemType = temp.Type;
        $scope.selectedItem = id;
        $scope.showEdit = true;
    };

    $scope.Edit = function (id) {
        $http.post("api/main/edit", JSON.stringify({ Id: id, Name: $scope.editItemName, Type: $scope.editItemType }))
            .then(function (response) {
                var temp = response.data;
                if (temp.ResultCode != 1) {
                    alert(temp.Message);
                }
                else {
                    RefreshTable($scope.currentPage, $scope.itemsCount);
                    $scope.showEdit = false;
                }
            });
    };

});

var statApp = angular.module('statApp', []);
statApp.controller('statController', function ($scope, $http) {
    $scope.types = { Type:"123", Count: 1 };
    $http.get("api/stat")
        .then(function (response) {
            $scope.types = response.data;
        });
});

angular.bootstrap(document.getElementById("stat-container"), ['statApp']);