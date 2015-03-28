(function() {
    angular.module("PermissionModule").controller("PermissionCtrl", [
        "$scope", "dataService", function ($scope, dataService) {
            $scope.selectedUsername = "";
            $scope.selectedUser = null;
            $scope.getUserPermission = function(username) {
                $scope.selectedUsername = username;

                dataService.getUserPermission(username).then(function(user) {
                    $scope.selectedUser = user;
                }, function(msg) {
                    alert("Failed to get user permssion, error: " + msg);
                });
            }
        }
    ]);
})();