var app = angular.module('mainApp', []);
app.controller('mainController', function ($scope, $http) {
    $scope.itemsCount = 4;
    $scope.currentPage = 1;
    $scope.count = new Array();
    $scope.showEdit = false;
    function RefreshTable(refreshPages) {
        $http.post("api/main", JSON.stringify({ Page: $scope.currentPage, Count: $scope.itemsCount}))
            .then(function (response) {
                $scope.items = response.data;
                if (refreshPages)
                {
                    $http.post("api/main/pages", JSON.stringify({ Count: $scope.itemsCount }))
                        .then(function (response) {
                            if (response.data.ResultCode < 0) {
                                alert(response.data.Message);
                                return;
                            }
                            else
                            {
                                $scope.count = new Array();
                                for (var i = 0; i < response.data.ResultCode; i++) {
                                    $scope.count.push(i + 1);
                                }
                            }
                        });
                }
                $("#pages-list li a").each(function () {
                    if (parseInt($(this).text()) == $scope.currentPage) {
                        $(this).css("color", "red");
                    }
                    else {
                        $(this).css("color", "#428bca");
                    }
                });
            });
    }

    RefreshTable(true);

    $scope.AddNewItem = function () {
        if ($scope.newItemName != "" && $scope.newItemType != "" && $scope.newItemName != null && $scope.newItemType) {
            $http.post("api/main/add", JSON.stringify({ Name: $scope.newItemName, Type: $scope.newItemType }))
                .then(function (response) {
                    var temp = response.data;
                    if (temp.ResultCode != 1) {
                        alert(temp.Message);
                    }
                    else {
                        RefreshTable(true);
                        $scope.newItemName = "";
                        $scope.newItemType = "";
                    }
                });
        }
    };

    $scope.DeleteItem = function (id) {
        $http.post("api/main/delete", JSON.stringify({ Id: id }))
            .then(function (response) {
                var temp = response.data;
                if (temp.ResultCode != 1) {
                    alert(temp.Message);
                }
                else {
                    RefreshTable(true);
                }
            });
    };

    $scope.EditItem = function (id) {
            var temp = $scope.items.find(function (item) { return item.Id == id; });
            $scope.editItemName = temp.Name;
            $scope.editItemType = temp.Type;
            $scope.selectedItem = id;
            $scope.showEdit = true;
    };

    $scope.Edit = function (id) {
        if ($scope.editItemName != "" && $scope.editItemType != "" && $scope.editItemName != null && $scope.editItemType != null) {
            $http.post("api/main/edit", JSON.stringify({ Id: id, Name: $scope.editItemName, Type: $scope.editItemType }))
                .then(function (response) {
                    var temp = response.data;
                    if (temp.ResultCode != 1) {
                        alert(temp.Message);
                    }
                    else {
                        RefreshTable(false);
                        $scope.showEdit = false;
                    }
                });
        }
    };

    $scope.SwitchPage = function (page) {
        $scope.currentPage = page;
        RefreshTable(false);
    }

});

var statApp = angular.module('statApp', []);
statApp.controller('statController', function ($scope, $http) {
    $scope.currentPage = 1;
    $scope.itemsCount = 3;
    function Refresh() {
        $http.post("api/stat", JSON.stringify({ Page: $scope.currentPage, Count: $scope.itemsCount }))
            .then(function (response) {
                $scope.types = response.data;
                $http.post("api/stat/pages", JSON.stringify({ Count: $scope.itemsCount }))
                    .then(function (response) {
                        if (response.data.ResultCode < 0) {
                            alert(response.data.Message);
                            return;
                        }
                        else {
                            $scope.count = new Array();
                            for (var i = 0; i < response.data.ResultCode; i++) {
                                $scope.count.push(i + 1);
                            }
                        }
                    });
                $("#pages-list li a").each(function () {
                    if (parseInt($(this).text()) == $scope.currentPage) {
                        $(this).css("color", "red");
                    }
                    else {
                        $(this).css("color", "#428bca");
                    }
                });
            });
    }
    $scope.RefreshStatTable = Refresh;

    $scope.SwitchPage = function (page) {
        $scope.currentPage = page;
        Refresh();
    }
    
});

angular.bootstrap(document.getElementById("stat-container"), ['statApp']);