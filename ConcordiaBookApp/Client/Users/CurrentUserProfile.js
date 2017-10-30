angular.module('bookApp')
.controller("CurrentUserController", CurrentUserController);

function CurrentUserController($scope, UserService) {
    getUserProfile();

    function getUserProfile() {
        $scope.isLoading = true;
        var user = UserService.getUser().then(function (data) {
            $scope.isLoading = false;
            $scope.user = data;
            $scope.showBooks = data.BooksForSale.length;
        });
    }
}