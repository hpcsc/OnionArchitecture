(function () {
    angular.module("PermissionModule").directive("resourceTree", ["$rootScope", function ($rootScope) {
        return {
            restrict: "E",
            scope: {
                resources: "="
            },
            template: "<div></div>",
            replace: true,
            link: function (scope, elm, attrs) {
                var unwatch = scope.$watch("resources", function (resources) {
                    if (angular.isDefined(resources)) {
                        unwatch();
                        init(); 
                    }
                });

                function init() {
                    elm.treeview({
                        data: scope.resources,
                        onNodeSelected: function (event, data) {
                            $rootScope.$broadcast("resourceTreeNodeClicked", data);
                        }
                    });
                }
            }
        };
    }]);
})();