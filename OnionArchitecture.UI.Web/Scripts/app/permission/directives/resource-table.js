(function () {
    angular.module("PermissionModule").directive("resourceTable", ["ngTableParams", "dataService", function (ngTableParams, dataService) {
        function handleError(msg) {
            alert("Failed to get resource detail, error: " + msg);
        }

        return {
            restrict: "E",
            templateUrl: "/Scripts/app/permission/directives/resource-table.html",
            controller: function ($scope) {
                $scope.tableParams = new ngTableParams({
                    page: 1,
                    count: 10
                }, {
                    groupBy: 'type',
                    getData: function ($defer, params) {
                        if ($scope.selectedNode) {
                            dataService.getResourceDetail($scope.selectedNode.id).then(function (resource) {
                                $defer.resolve(resource.permissions);
                                $scope.currentResource = resource;
                            }, handleError);
                        }
                    }
                });

                $scope.$on("resourceTreeNodeClicked", function (e, data) {
                    if (data && data.id) {
                        $scope.selectedNode = data;

                        $scope.tableParams.reload()
                    }
                });

                $scope.isChecked = function (permission, toCheckAgainst) {
                    return (permission & toCheckAgainst) == toCheckAgainst;
                }

                $scope.updateResource = function () {

                }
            }
        };
    }]);
})();