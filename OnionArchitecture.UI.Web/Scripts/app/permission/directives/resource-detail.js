(function () {
    angular.module("PermissionModule").directive("resourceDetail", ["ngTableParams", "dataService", "$rootScope", "toaster",
            function (ngTableParams, dataService, $rootScope, toaster) {

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
                            }, function (msg) {
                                toaster.pop("danger", "Error", "Error: " + msg);
                            });
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
                        if (permission == 16) {
                            object.permission = 0;
                        }
                        else {
                            object.permission -= permission;
                        }
                    } else {
                        if (permission == 16) {
                            object.permission = 16;
                        }
                        else {
                            object.permission += permission;
                        }
                    }                    
                }

                $scope.updateResource = function () {
                    var data = {
                        id: $scope.currentResource.resourceId,
                        name: $scope.currentResource.resourceName,
                        description: $scope.currentResource.resourceDescription,
                        permissions: []
                    };

                    $($scope.tableParams.data).each(function (i, group) {                        
                        $(group.data).each(function (j, obj) {                            
                            data.permissions.push({
                                permissionId: obj.permissionId,
                                id: obj.id,
                                isRole: obj.type == "Role",
                                permission: obj.permission
                            });
                        });
                    });

                    dataService.updateResource(data).then(function (response) {
                        toaster.pop("success", "Success", "Resource updated successfully");
                        $rootScope.$broadcast("resourceUpdated");
                    }, function (msg) {
                        toaster.pop("danger", "Error", "Error: " + msg);
                    });
                }
            }
        };
    }]);
})();