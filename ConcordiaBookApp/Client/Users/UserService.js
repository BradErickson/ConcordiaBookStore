angular.module('bookApp')
.factory('UserService', UserService);

function UserService($http) {
    var UserService = {};

    UserService.postUser = function (CreateUserViewModel) {
        return $http({
            method: 'POST',
            url: '/Account/RegisterNewUser',
            data: CreateUserViewModel,
            headers: {
                "Content-Type": "application/json"
            }
        }).then(function successCallback(response) {
            return response;
        }, function errorCallback(response) {
            return alert("This request was unsucessful please check to make sure all of your information inside the form is correct and try again.");
        });
    };


    UserService.getUser = function () {
        return $http({
            method: 'GET',
            url: '/user/GetCurrentUser'
        }).then(function successCallback(response) {
            return response.data;
        }, function errorCallback(response) {
            console.log(response);
        });
    };

    return UserService;

}