angular.module('bookApp')
.controller("CurrentUserController", CurrentUserController)

function CurrentUserController($scope, UserService) {
    getUserProfile();

    function getUserProfile() {
        var user = UserService.getUser().then(function (data) {
            $scope.user = data;
        });
    }
}