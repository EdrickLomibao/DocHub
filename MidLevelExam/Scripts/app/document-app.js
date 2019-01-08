var documentapp = angular.module('DocumentApp', ['ngRoute']) 

documentapp.config(["$provide", function ($provide) {
    var rootUrl = $("#linkRoot").attr("href");
    $provide.constant('rootUrl', rootUrl);
}]);

documentapp.controller("DocumentCtrl", function ($scope, $http, $window, $timeout, rootUrl, $location) {

    $scope.documents = [];
    $scope.users = [];

    $http.get('document/GetDocuments').then(function (response) {

        $scope.documents = response.data;
    })

    $http.get('document/GetUsers').then(function (response) {

        $scope.users = response.data;
    })

    $scope.collaborate = function (doc) {
        setTimeout(function () { document.location.href = 'Document/Editor?id=' + doc.ID }, 500);
    };

    $scope.sendinvitation = function (doc) {
        
        $http.post('/document/SendInvitation', {
            'Email': $scope.useraccount,
            'Url': 'http://' + location.host + '/Account/Register?sourcedocument=' + doc.ID
        }).then(function (response) {

            if (response.data.Status) { 
                $("#SuccessMessage").removeClass("hidden");
                $("#SuccessMessage").text(response.data.Message);
                $("#ErrorMessage").addClass("hidden");
            }
            

        }, function (response) {
            alert(response.data);
            $("#ErrorMessage").text(response.data.Message);
            $("#ErrorMessage").removeClass("hidden");
        });

    };

    // save the selected user account to hidden input for sending of email
    $scope.selectaccount = function (user) {

        $scope.useraccount = user.Email;
        
    };


});