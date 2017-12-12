angular.module('bookApp')
.controller("CreateNewUserController", CreateNewUserController);

function CreateNewUserController($scope, UserService) {
    $scope.CreateUserViewModel = {};
    $scope.confirmation = "";
    $scope.isLoading = false;
    $scope.update = function (CreateUserViewModel) {
        $scope.isLoading = true;
        UserService.postUser(CreateUserViewModel).then(function (response) {
            if (response.data.error) {
                $scope.isLoading = false;
                alert(response.data.error.message);
            } else {
                $scope.isLoading = false;
                $scope.confirmation = "To complete registration, click the confirmation link in your email";
            }
        });
    };
}