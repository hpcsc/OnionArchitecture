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
            $scope.users = model.users;
            $scope.resources = mapToTreeModel(model.resources);
        }, handleError);
    }

    angular.module("PermissionModule").controller("PermissionCtrl", [
        "$scope", "dataService", function ($scope, dataService) {
            $scope.selectedUsername = "";
            $scope.selectedUser = null;
            $scope.selectedResource = null;


            $scope.getUserPermission = function(username) {
                $scope.selectedUsername = username;

                dataService.getUserPermission(username).then(function(user) {
                    $scope.selectedUser = user;
                }, handleError);
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