angular.module('bookApp')
.controller("CreateNewUserController", CreateNewUserController);

function CreateNewUserController($scope, UserService) {
    $scope.CreateUserViewModel = {};
    $scope.update = function (CreateUserViewModel) {
        UserService.postUser(CreateUserViewModel).then(function (response) {
            debugger;
            //window.location = "/";
        });
    };
}