(function (angular) {
    angular.module('settlementModule').controller('StudentController', StudentController);

    StudentController.$inject = ['$scope', '$filter', 'studentDataService', '$routeParams', 'studentInfo', 'violations', 'hostels', 'rooms', '$location', '$timeout'];

    function StudentController($scope, $filter, studentDataService, $routeParams, studentInfo, violations, hostels, rooms, $location, $timeout) {
        var vm = this;

        vm.studentInfo = studentInfo;
        vm.violationsList = violations;
        vm.hostels = hostels;
        vm.rooms = rooms;
        vm.currentViolation = violations[0];
        
        $scope.showNotification = false;
        $scope.showWarning = false;
 
        vm.currentTab = $location.hash();
        vm.modelChanged = false; // Indicates whether data in the model was changed
        vm.checkInBoxOpened = false;
        vm.addPayBoxOpened = false;

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
                vm.modelChanged = false;
                $timeout($scope.closePopUp, 4000);
            })
            .error(function (res) {
                $scope.showNotifyPopUp('Error: student data was not saved!')
                $timeout($scope.closePopUp, 4000);
            });
            
        };

        vm.addViolation = function () {
            var result = vm.studentInfo.Violations.filter(function (v) {
                return v.Name === vm.currentViolation.Name;
            })[0];
            if (result == undefined) {
                vm.studentInfo.Violations.push(vm.currentViolation);
                vm.modelChanged = true;
            }
            else {
                $scope.showNotifyPopUp('Error: there is such violation already!')
                $timeout($scope.closePopUp, 3000);
            }
        }

        vm.clearViolations = function () {
            vm.studentInfo.Violations = [];
        }

        vm.cancelProfile = function () {
            activate();
            vm.modelChanged = false;
        }; // Cancel unsaved changes in the profile

        vm.validationCheck = function () {
            return vm.studentInfo.Name && vm.studentInfo.Surname && vm.studentInfo.Institute && vm.modelChanged == true
            vm.studentInfo.Group;
        };

        vm.checkIn = function() {
            studentDataService.checkIn(vm.studendInfo.Id, vm.chosenRoom);
        }

        vm.checkOut = function () {
            studentDataService.checkOut(vm.studentInfo.Id);
        }

        vm.addPay = function () {

        }

    };
})(angular);