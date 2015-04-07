(function () {
    angular.module("PermissionModule").directive("userResourcePermission", ["ngTableParams", function (ngTableParams) {
        return {
            restrict: "E",
            scope: {
                permissions: "="
            },
            templateUrl: "/Scripts/app/permission/directives/user-resource-permission.html",
            controller: function ($scope) {
                $scope.tableParams = new ngTableParams({
                    page: 1,
                    count: 10
                }, {
                    getData: function ($defer, params) {
                        if ($scope.permissions && $scope.permissions.length > 0) {
                            $defer.resolve($scope.permissions);
                        }
                    }
                });

                $scope.$watchCollection("permissions", function () {
                    $scope.tableParams.reload();
                });

                $scope.isChecked = function (permission, toCheckAgainst) {
                    return (permission & toCheckAgainst) == toCheckAgainst;
                }

                $scope.updatePermission = function (object, permission) {
                    if ($scope.isChecked(object.type, permission)) {
                        object.type -= permission;
                    } else {
                        object.type += permission;
                    }
                }
            }
        };
    }]);
})();