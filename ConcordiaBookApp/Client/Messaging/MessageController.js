angular.module('bookApp')
.controller("CreateMessageController", CreateMessageController)

function CreateMessageController($scope, MessageService) {
    $scope.isLoading = false;
    $scope.isReply = getParameterByName("isReply");

    $scope.update = function (PostMessage) {
        $scope.isLoading = true
        var bookId = getParameterByName("bookId");
        MessageService.postMessage(PostMessage, bookId).then(function (response) {
            $scope.isLoading = false;
            window.location = "/";
        });
    }

    $scope.reply = function (PostMessage) {
        $scope.isLoading = true
        var messageId = getParameterByName("messageId");
        MessageService.replyMessage(PostMessage, messageId).then(function (response) {
            $scope.isLoading = false;
            window.location = "/";
        });
    }
    
    function getParameterByName(queryStringName) {
        var urlString = window.location.href;
        queryStringName = queryStringName.replace(/[\[\]]/g, "\\$&");
        var regex = new RegExp("[?&]" + queryStringName + "(=([^&#]*)|&|#|$)"),
            results = regex.exec(urlString);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, " "));
    }

}