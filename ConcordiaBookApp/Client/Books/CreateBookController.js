angular.module('bookApp')
    .controller("CreateBookController", CreateBookController)

function CreateBookController($scope, BookService) {
    $scope.PostBook = {};
    $scope.isLoading = false;
    $scope.update = function (PostBook) {
        $scope.isLoading = true
        BookService.postBooks(PostBook).then(function (response) {
            $scope.isLoading = false;
            alert("Success");
            window.location = "/";
        });
    }


}