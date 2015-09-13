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
        },
            
        updateResource = function(data) {
            return post('/permission/updateResource', data);
        },
            
        addResource = function (data) {
            return post('/permission/addResource', data);
        },
            
        searchUser = function (input) {
            return get('/permission/searchUser?input=' + input);
        },
            
        updateUserRolesAndPermission = function (data) {
            return post('/permission/updateUserRolesAndPermission', data);
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

        function post(url, data) {
            var deferred = $q.defer();

            $http.post(url, data)
               .success(function (response) {
                   if (response.success) {                       
                       deferred.resolve(response);
                   } else {                       
                       deferred.reject(response.errors.join("<br/>"));
                   }
               }).error(function (msg, code) {
                   deferred.reject(msg);
               });

            return deferred.promise;
        }

        return {
            getUserPermission: getUserPermission,
            getInitialIndexModel: getInitialIndexModel,
            getResourceDetail: getResourceDetail,
            updateResource: updateResource,
            addResource: addResource,
            searchUser: searchUser,
            updateUserRolesAndPermission: updateUserRolesAndPermission
        };
    }]);
})();