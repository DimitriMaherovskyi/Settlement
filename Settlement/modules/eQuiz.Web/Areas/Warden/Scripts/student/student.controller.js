﻿(function (angular) {
    angular.module('settlementModule').controller('StudentController', StudentController);

    StudentController.$inject = ['$scope', '$filter', 'studentDataService', '$routeParams', 'studentInfo', '$location', '$timeout'];

    function StudentController($scope, $filter, studentDataService, $routeParams, studentInfo, $location, $timeout) {
        var vm = this;

        vm.studentInfo = studentInfo;
        var map = { 17: false, 13: false };
        $scope.showNotification = false;
        $scope.showWarning = false;

 
        vm.currentTab = $location.hash();
        vm.modelChanged = false; // Indicates whether data in the model was changed


        function activate() {
            studentDataService.getStudentInfo($location.search().Id).then(function (response) {
                vm.studentInfo = response.data;
                return vm.studentInfo;
            });
            generatePredicate();
            }

        vm.saveProfile = function () {
            studentDataService.saveProfileInfo(vm.studentInfo.id, vm.studentInfo.firstName, vm.studentInfo.lastName, vm.studentInfo.phone)
            .success(function (res) {
                $scope.showNotifyPopUp('Profile data was sucessfully saved!')
                $timeout($scope.closePopUp, 5000);
            })
            .error(function (res) {
                $scope.showNotifyPopUp('Error: profile data was not saved!')
                $timeout($scope.closePopUp, 5000);
            });
            vm.modelChanged = false;
        };

        vm.cancelProfile = function () {
            activate();
            vm.modelChanged = false;
        }; // Cancel unsaved changes in the profile

        vm.validationCheck = function () {
            return $scope.profileInfo.firstName.$valid && $scope.profileInfo.lastName.$valid && $scope.profileInfo.phone.$valid && vm.modelChanged;
        }; 
    };
})(angular);