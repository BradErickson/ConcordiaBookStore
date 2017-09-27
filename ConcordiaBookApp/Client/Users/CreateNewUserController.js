angular.module('bookApp')
.controller("CreateNewUserController", CreateNewUserController)

function CreateNewUserController($scope, UserService) {
    $scope.CreateUserViewModel = {};
    $scope.update = function (CreateUserViewModel) {
        debugger;
        UserService.postUser(CreateUserViewModel).then(function (response) {
                alert("Success");
                window.location = "/";

        });
    }
}