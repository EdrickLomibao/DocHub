var loginapp = angular.module('LoginApp', ['ngRoute']);

loginapp.config(["$provide", function ($provide) {
    var rootUrl = $("#linkRoot").attr("href");
    $provide.constant('rootUrl', rootUrl);
}]);

loginapp.controller("LoginCtrl", function ($scope, $http, $window, $timeout, rootUrl) {

    $scope.login = function () {

        $http.post('/account/login', {
            'Email': $scope.email,
            'Password': $scope.password
        })
            .then(function(response){
                if (response.data.Status) {
                    $window.location.href = rootUrl + 'Document';

                } else {
                    $("#ErrorMessage").text(response.data.Message);
                    $("#ErrorMessage").removeClass("hidden");
                }
            }

    )};

});