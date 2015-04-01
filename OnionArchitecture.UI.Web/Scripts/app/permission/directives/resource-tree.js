(function () {
    angular.module("PermissionModule").directive("resourceTree", ["$rootScope", "dataService", function ($rootScope, dataService) {
        return {
            restrict: "E",
            scope: {
                resources: "="
            },
            templateUrl: "/Scripts/app/permission/directives/resource-tree.html",
            replace: true,
            link: function (scope, elm, attrs) {
                var unwatch = scope.$watch("resources", function (resources) {
                    if (angular.isDefined(resources)) {
                        //unwatch();
                        init(); 
                    }
                });

                function init() {
                    elm.find(".resource-tree").treeview({
                        data: scope.resources,
                        levels: 3,
                        onNodeSelected: function (event, data) {
                            scope.selectedNode = data;
                            $rootScope.$broadcast("resourceTreeNodeClicked", data);
                        }
                    });
                }

                scope.newResourceName = "";
                scope.addResource = function () {
                    dataService.addResource({
                        name: scope.newResourceName,
                        parentId: scope.selectedNode ? scope.selectedNode.id : null
                    }).then(function (response) {
                        if (response.success) {
                            scope.newResourceName = "";
                            $rootScope.$broadcast("newResourceAdded");
                        }
                        else {
                            alert("Failed to add resource, error: " + msg);
                        }
                    },
                    function (msg) {
                        alert("Failed to add resource, error: " + msg);
                    })
                }
            }
        };
    }]);
})();