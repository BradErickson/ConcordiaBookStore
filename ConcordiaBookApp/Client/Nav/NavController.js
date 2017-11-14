angular.module('bookApp')
.controller("NavController", NavController)

function NavController($scope, $http) {
    $scope.showNav = getIsLogin();
    function getIsLogin() {
        var isLoggedIn = jQuery("#isLoggedIn").val();
        if (isLoggedIn == "") {
            return true;
        } else {
            return false;
        }
    }
    
}