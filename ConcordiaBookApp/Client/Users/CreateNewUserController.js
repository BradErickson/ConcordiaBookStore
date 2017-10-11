angular.module('bookApp')
.controller("CreateNewUserController", CreateNewUserController);

function CreateNewUserController($scope, UserService) {
    $scope.CreateUserViewModel = {};
    $scope.error = "";
    $scope.update = function (CreateUserViewModel) {
        UserService.postUser(CreateUserViewModel).then(function (response) {
            if (response.data.error) {
                $scope.error = response.data.error.message;
            } else {
                window.location = "/";
            }
        });
    };
}