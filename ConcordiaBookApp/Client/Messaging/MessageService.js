angular.module('bookApp')
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

    return MessageService;

}