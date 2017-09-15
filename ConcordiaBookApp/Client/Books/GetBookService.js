angular.module('bookApp')
.factory('BookService', BookService)

function BookService ($http) {
    var BookService = {};
    BookService.getBooks = function () {
        return $http({
            method: 'GET',
            url: '/Books/getBooks'
        }).then(function successCallback(response) {
            return response.data;
        }, function errorCallback(response) {
            console.log(response);
        });
    };
    return BookService;

}