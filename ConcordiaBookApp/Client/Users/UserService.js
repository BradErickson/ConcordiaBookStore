﻿angular.module('bookApp')
.factory('UserService', UserService)

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
        });
    }


    UserService.getUser = function () {
        return $http({
            method: 'GET',
            url: '/user/GetCurrentUser'
        }).then(function successCallback(response) {
            return response.data;
        }, function errorCallback(response) {
        });
    };

    return UserService;

}