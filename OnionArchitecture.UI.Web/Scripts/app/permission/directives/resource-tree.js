(function () {
    angular.module("PermissionModule").directive("resourceTree", ["$rootScope", "dataService", "toaster",
        function ($rootScope, dataService, toaster) {

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
                            toaster.pop("error", "Error", response.errors.join("<br/>"));
                        }
                    },
                    function (msg) {
                        toaster.pop("error", "Error", msg);
                    })
                }
            }
        };
    }]);
})();