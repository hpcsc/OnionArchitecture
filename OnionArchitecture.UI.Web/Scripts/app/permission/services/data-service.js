(function() {
    angular.module("PermissionModule").factory("dataService", ["$http", "$q", function ($http, $q) {
        var getUserPermission = function(username) {
            var deferred = $q.defer();

            $http.get('/permission/getUserPermission?username=' + username)
               .success(function (data) {
                   deferred.resolve(data);
               }).error(function (msg, code) {
                   deferred.reject(msg);                   
               });

            return deferred.promise;
        };

        return {
            getUserPermission: getUserPermission
        };
    }]);
})();