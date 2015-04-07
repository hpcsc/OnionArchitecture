(function () {
    angular.module("PermissionModule").directive("userResourcePermission", ["ngTableParams", function (ngTableParams) {
        return {
            restrict: "E",
            scope: {
                userPermissions: "=",
                rolePermissions: "="
            },
            templateUrl: "/Scripts/app/permission/directives/user-resource-permission.html",
            controller: function ($scope) {
                $scope.userTableParams = new ngTableParams({
                    page: 1,
                    count: 10
                }, {
                    getData: function ($defer, params) {
                        if ($scope.userPermissions && $scope.userPermissions.length > 0) {
                            $defer.resolve($scope.userPermissions);
                        }
                    }
                });

                $scope.roleTableParams = new ngTableParams({
                    page: 1,
                    count: 10
                }, {
                    getData: function ($defer, params) {
                        if ($scope.rolePermissions && $scope.rolePermissions.length > 0) {
                            $defer.resolve($scope.rolePermissions);
                        }
                    }
                });

                $scope.$watchCollection("userPermissions", function () {
                    $scope.userTableParams.reload();
                });

                $scope.isChecked = function (permission, toCheckAgainst) {
                    return (permission & toCheckAgainst) == toCheckAgainst;
                }

                $scope.updatePermission = function (object, permission) {
                    if ($scope.isChecked(object.type, permission)) {
                        if (permission == 16) {
                            object.type = 0;
                        }
                        else {
                            object.type -= permission;
                        }
                    } else {
                        if (permission == 16) {
                            object.type = 16;
                        }
                        else {
                            object.type += permission;
                        }
                    }
                }
            }
        };
    }]);
})();