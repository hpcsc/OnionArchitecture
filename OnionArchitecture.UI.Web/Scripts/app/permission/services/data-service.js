(function() {
    angular.module("PermissionModule").factory("dataService", ["$http", "$q", function ($http, $q) {
        var getUserPermission = function (username) {
            return get('/permission/getUserPermission?username=' + username);
        },

        getInitialIndexModel = function () {
            return get('/permission/getInitialIndexModel');
        },

        getResourceDetail = function (id) {
            return get('/permission/getResourceDetail?id=' + id);
        };

        function get(url) {
            var deferred = $q.defer();

            $http.get(url)
               .success(function (data) {
                   deferred.resolve(data);
               }).error(function (msg, code) {
                   deferred.reject(msg);
               });

            return deferred.promise;
        }

        return {
            getUserPermission: getUserPermission,
            getInitialIndexModel: getInitialIndexModel,
            getResourceDetail: getResourceDetail
        };
    }]);
})();