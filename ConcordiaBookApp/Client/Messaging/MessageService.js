﻿angular.module('bookApp')
.factory('MessageService', MessageService)

function MessageService($http) {
    var MessageService = {};
    MessageService.postMessage = function (PostMessage, bookId) {
        return $http({
            method: 'POST',
            url: '/Books/SendMessage/' + bookId,
            data: PostMessage,
            headers: {
                "Content-Type": "application/json"
            }
        }).then(function successCallback(response) {
            console.log("response", response);
        }, function errorCallback(response) {
            console.log("response", response);
        });
    }

    MessageService.getMyMessagePage = function () {
        window.location = "/messages/GetMyMessages";
    }

    MessageService.getMyMessages = function () {
        return $http({
            method: 'GET',
            url: '/Messages/GetMyMessages',
            headers: {
                "Content-Type": "application/json"
            }
        }).then(function successCallback(response) {
            return response.data;
        }, function errorCallback(response) {
            console.log("response", response);
        });
    }

    MessageService.replyMessage = function (PostMessage, messageId) {
        return $http({
            method: 'POST',
            url: '/Messages/ReplyMessage/' + messageId,
            data: PostMessage,
            headers: {
                "Content-Type": "application/json"
            }
        }).then(function successCallback(response) {
            console.log("response", response);
        }, function errorCallback(response) {
            console.log("response", response);
        });
    }
    
    return MessageService;

}