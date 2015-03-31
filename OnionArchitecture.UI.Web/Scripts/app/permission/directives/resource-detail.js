(function () {
    angular.module("PermissionModule").directive("resourceDetail", ["ngTableParams", "dataService", "$rootScope", function (ngTableParams, dataService, $rootScope) {
        function handleError(msg) {
            alert("Error: " + msg);
        }

        return {
            restrict: "E",
            templateUrl: "/Scripts/app/permission/directives/resource-detail.html",
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

                        $scope.tableParams.reload();
                    }
                });

                $scope.isChecked = function (permission, toCheckAgainst) {
                    return (permission & toCheckAgainst) == toCheckAgainst;
                }

                $scope.updatePermission = function (object, permission) {
                    if ($scope.isChecked(object.permission, permission)) {
                        object.permission -= permission;
                    } else {
                        object.permission += permission;
                    }                    
                }

                $scope.updateResource = function () {
                    console.log($scope.tableParams.data);
                    var data = {
                        id: $scope.currentResource.resourceId,
                        name: $scope.currentResource.resourceName,
                        description: $scope.currentResource.resourceDescription,
                        permissions: []
                    };

                    $($scope.tableParams.data).each(function (i, group) {                        
                        $(group.data).each(function (j, obj) {                            
                            data.permissions.push({
                                id: obj.id,
                                isRole: obj.type == "Role",
                                permission: obj.permission
                            });
                        });
                    });

                    dataService.updateResource(data).then(function(response) {
                        alert("Resource updated successfully");
                        $rootScope.$broadcast("resourceUpdated");
                    }, handleError);
                }
            }
        };
    }]);
})();