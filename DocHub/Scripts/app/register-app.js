var accountapp = angular.module('RegisterApp', ['ngRoute'], function ($locationProvider) {
    $locationProvider.html5Mode(true); // see also the <base href="/" /> in <head>
});

accountapp.controller("RegisterCtrl", function ($scope, $http, $window, $timeout, $location) {

    $scope.register = function () {

        $scope.location = $location;
        $scope.sourcedocument = $scope.location.search()['sourcedocument'];

        $http.post('/account/register', {
            'Email': $scope.email,  
            'Password': $scope.password,
            'ConfirmPassword': $scope.confirmpassword,
            'Firstname': $scope.firstname,
            'Lastname': $scope.lastname
        }).then(function (response) {
            if (response.data.Status) {

                $("#SuccessMessage").removeClass("hidden");
                $("#ErrorMessage").addClass("hidden");

                // Redirect to Editor based on sourcedocument query string.
                if ($scope.sourcedocument != undefined) {
                    $timeout(function () { $window.location.href = 'document/Editor?id=' + $scope.sourcedocument; }, 3000);
                }
                else {
                    $timeout(function () { $window.location.href = 'document'; }, 3000);
                }


            } else {
                $("#ErrorMessage").text(response.data.Message);
                $("#ErrorMessage").removeClass("hidden");
            }

        }, function (response) {
            $("#ErrorMessage").text(data.Message);
            $("#ErrorMessage").removeClass("hidden");
        });


    };

});