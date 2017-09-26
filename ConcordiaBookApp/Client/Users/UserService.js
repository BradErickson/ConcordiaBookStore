angular.module('bookApp')
.factory('UserService', UserService)

function UserService($http) {
    var UserService = {};

    UserService.postUser = function (CreateUserViewModel) {
        return $http({
            method: 'POST',
            url: '../User/RegisterNewUser',
            data: CreateUserViewModel,
            headers: {
                "Content-Type": "application/json"
            }
        }).then(function successCallback(response) {
            console.log("response", response);
        }, function errorCallback(response) {
            console.log("response", response);
        });
    }


    return UserService;

}