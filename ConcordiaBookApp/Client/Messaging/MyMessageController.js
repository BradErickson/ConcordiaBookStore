angular.module('bookApp')
.controller("MyMessageController", MyMessageController)

function MyMessageController($scope, MessageService) {
    $scope.isLoading = false;
    $scope.myMessages = {};

    $scope.reply = function (PostMessage) {
        $scope.isLoading = true;
        var messageId = getParameterByName("messageId");
        MessageService.replyMessage(PostMessage, messageId).then(function (response) {
            $scope.isLoading = false;
            alert("Success");
            $scope.messages = {};
        });
    }
    getMyMessages();

    function getMyMessages() {
        MessageService.getMyMessages().then(function (data) {
            debugger;
            $scope.myMessages = data;
        })
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