angular.module('bookApp')
.controller("MyMessageController", MyMessageController)

function MyMessageController($scope, MessageService) {
    $scope.isLoading = false;
    $scope.myMessages = {};

    $scope.reply = function (PostMessage) {
        debugger;
        MessageService.replyMessagePage(PostMessage);
    }
    getMyMessages();

    function getMyMessages() {
        MessageService.getMyMessages().then(function (data) {
            debugger;
            $scope.myMessages = data;
        })
    }
}