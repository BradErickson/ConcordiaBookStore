angular.module('bookApp')
.controller("CreateNewUserController", CreateNewUserController);

function CreateNewUserController($scope, UserService) {
    $scope.CreateUserViewModel = {};
    $scope.error = "";
    $scope.isLoading = false;
    $scope.update = function (CreateUserViewModel) {
        $scope.isLoading = true;
        UserService.postUser(CreateUserViewModel).then(function (response) {
            if (response.data.error) {
                $scope.isLoading = false;
                $scope.error = response.data.error.message;
            } else {
                $scope.isLoading = false;
                window.location = "/";
            }
        });
    };
}