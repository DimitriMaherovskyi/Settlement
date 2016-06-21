(function (angular) {
    angular.module('settlementModule').controller('StudentController', StudentController);

    StudentController.$inject = ['$scope', '$filter', 'studentDataService', '$routeParams', 'studentInfo', 'violations', '$location', '$timeout'];

    function StudentController($scope, $filter, studentDataService, $routeParams, studentInfo, violations, $location, $timeout) {
        var vm = this;

        vm.studentInfo = studentInfo;
        vm.violationsList = violations;
        
        $scope.showNotification = false;
        $scope.showWarning = false;
 
        vm.currentTab = $location.hash();
        vm.modelChanged = false; // Indicates whether data in the model was changed


        function activate() {
            studentDataService.getStudentInfo($location.search().Id).then(function (response) {
                vm.studentInfo = response.data;
                return vm.studentInfo;
            });
            studentDataService.getViolations().then(function (response) {
                vm.violationsList = response.data;
                return vm.violationsList;
            });

            }

        vm.saveProfile = function () {
            studentDataService.saveProfileInfo(vm.studentInfo)
            .success(function (res) {
                $scope.showNotifyPopUp('Student data was sucessfully saved!')
                $timeout($scope.closePopUp, 4000);
            })
            .error(function (res) {
                $scope.showNotifyPopUp('Error: student data was not saved!')
                $timeout($scope.closePopUp, 4000);
            });
            vm.modelChanged = false;
        };

        vm.addViolation = function () {
            vm.currentViolation && vm.studentInfo.violations.push(vm.currentViolation);
        }

        vm.clearViolations = function () {
            vm.studentInfo.violations = [];
        }

        vm.cancelProfile = function () {
            activate();
            vm.modelChanged = false;
        }; // Cancel unsaved changes in the profile

        vm.validationCheck = function () {
            return true; //$scope.studentInfo.firstName.$valid && $scope.studentInfo.lastName.$valid && $scope.Info.phone.$valid && vm.modelChanged;
        };

        vm.toggleAddViolationFrame = function () {

        }
    };
})(angular);