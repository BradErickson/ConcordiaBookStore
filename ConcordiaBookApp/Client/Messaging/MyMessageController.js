angular.module('bookApp')
.controller("MyMessageController", MyMessageController)

function MyMessageController($scope, MessageService) {
    $scope.isLoading = false;
    $scope.myMessages = {};

    $scope.reply = function (PostMessage) {
        MessageService.replyMessagePage(PostMessage);
    }
    getMyMessages();

    function getMyMessages() {
        MessageService.getMyMessages().then(function (data) {
            $scope.myMessages = data;
        })
    }
}