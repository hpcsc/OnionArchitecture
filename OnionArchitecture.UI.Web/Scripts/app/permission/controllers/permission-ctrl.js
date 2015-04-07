(function () {
    function handleError(msg) {
        alert("Failed to get user permssion, error: " + msg);
    }

    function mapToTreeModel(resources) {
        if (!resources || resources.length == 0) {
            return null;
        }

        return $(resources).map(function (i, r) {
            var node = {
                text: r.name,
                nodes: [],
                id: r.id,
                permissionId: r.permissionId
            };

            node.nodes = mapToTreeModel(r.children);

            return node;
        }).get();
    }

    function loadData(dataService, $scope) {
        dataService.getInitialIndexModel().then(function (model) {
            $scope.resources = mapToTreeModel(model.resources);
            $scope.availableRoles = model.availableRoles;
        }, handleError);
    }

    angular.module("PermissionModule").controller("PermissionCtrl", [
        "$scope", "dataService", function ($scope, dataService) {
            $scope.selectedUsername = "";
            $scope.selectedUser = null;
            $scope.searchUserInput = "";
            $scope.users = [];
            $scope.availableRoles = [];

            $scope.searchUser = function () {
                dataService.searchUser($scope.searchUserInput).
                    then(function (data) {
                        $scope.users = data;

                        $scope.showSearchResult = true;
                    }, handleError);
            }


            $scope.getUserPermission = function(username) {
                $scope.selectedUsername = username;

                dataService.getUserPermission(username).then(function(user) {
                    $scope.selectedUser = user;
                }, handleError);
            }

            $scope.updateUserRole = function (role) {
                var user = $scope.selectedUser;
                if ($scope.hasRole(user, role)) {
                    var index = -1;
                    for (var i = 0; i < user.roles.length; i++) {
                        if (user.roles[i].id == role.id) {
                            index = i;
                            break;
                        }
                    }

                    user.roles.splice(index, 1);
                }
                else {
                    user.roles.push(role);
                }
            }

            $scope.updateUserRolesAndPermission = function () {
                var data = {
                    userId: $scope.selectedUser.userId,
                    fullName: $scope.selectedUser.fullName,
                    userPermissions: $scope.selectedUser.userPermissions,
                    roles: $scope.selectedUser.roles
                };

                dataService.updateUserRolesAndPermission(data).then(function (response) {
                    if (response && response.success) {
                        //refresh
                    }
                    else {
                        handleError(response.errors.join("<br/>"));
                    }
                }, handleError);
            }

            $scope.hasRole = function (user, role) {
                for (var i = 0; i < user.roles.length; i++) {
                    if (user.roles[i].id == role.id) {
                        return true;
                    }
                }

                return false;
            }

            loadData(dataService, $scope);

            $scope.$on("resourceUpdated", function () {
                loadData(dataService, $scope);
            });

            $scope.$on("newResourceAdded", function () {
                loadData(dataService, $scope);
            });
        }
    ]);
})();