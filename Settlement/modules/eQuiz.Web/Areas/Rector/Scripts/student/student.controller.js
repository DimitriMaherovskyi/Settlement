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
            studentDataService.saveProfileInfo(vm.studentInfo.Id, vm.studentInfo.Name, vm.studentInfo.Surname, vm.studentInfo.Group, vm.studentInfo.Institute)
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
            studentDataService.addViolation(vm.studentInfo.Id, vm.currentViolation.Id)
               .success(function (res) {
                   activate();
                   $scope.showNotifyPopUp('Violation was successfully added!')
                   $timeout($scope.closePopUp, 4000);
               })
               .error(function (res) {
                   $scope.showNotifyPopUp('Error: violation was not added!')
                   $timeout($scope.closePopUp, 4000);
               });
        }

        vm.cancelProfile = function () {
            activate();
            vm.modelChanged = false;
        }; // Cancel unsaved changes in the profile

        vm.validationCheck = function () {
            return (vm.studentInfo.Name && vm.studentInfo.Surname && vm.studentInfo.Institute && (vm.modelChanged == true) && vm.studentInfo.Group) != undefined;
        };

        vm.checkInValidation = function() {
            return (vm.chosenHostel && vm.chosenRoom) != undefined;
        }

        vm.addPayValidation = function() {
            var condition1 = (vm.paySum && vm.payTillDate) != undefined;
            var condition2 = vm.payTillDate > Date.now();
            return condition1 && condition2;
        }

        vm.violationValidation = function() {
            return vm.currentViolation.Time != undefined && vm.currentViolation.Time > Date.now();
        }

        vm.addSettleRequest = function () {
            studentDataService.addSettleRequest(vm.studentInfo.Id)
            .success(function (res) {
                $scope.showNotifyPopUp('Student was successfully added to list!')
                $timeout($scope.closePopUp, 4000);
                activate();
            })
            .error(function (res) {
                $scope.showNotifyPopUp('Error: student was not added!')
                $timeout($scope.closePopUp, 4000);
            });
        }

        vm.checkIn = function () {
            studentDataService.checkIn(vm.studentInfo.Id, vm.chosenRoom.Id)
            .success(function (res) {
                vm.checkInBoxOpened = false;
                $scope.showNotifyPopUp('Student was successfully checked in!')
                $timeout($scope.closePopUp, 4000);
                activate();
            })
            .error(function (res) {
                $scope.showNotifyPopUp('Error: student was not checked in!')
                $timeout($scope.closePopUp, 4000);
            });
        }

        vm.checkOut = function () {
            studentDataService.checkOut(vm.studentInfo.Id)
            .success(function (res) {
                $scope.showNotifyPopUp('Student was successfully checked out!')
                $timeout($scope.closePopUp, 4000);
                activate();
            })
            .error(function (res) {
                $scope.showNotifyPopUp('Error: student was not checked out!')
                $timeout($scope.closePopUp, 4000);
            });
        }

        vm.addPay = function () {
            var hostelId = vm.hostels.filter(function (v) {
                return v.Number === vm.studentInfo.Hostel;
            })[0].Id;
            studentDataService.addPay(vm.paySum, vm.studentInfo.Id, hostelId, vm.payTillDate.toLocaleString())
                .success(function (res) {
                    vm.addPayBoxOpened = false;
                    $scope.showNotifyPopUp('Pay was successfully added!')
                    $timeout($scope.closePopUp, 4000);
                    activate();
                })
            .error(function (res) {
                $scope.showNotifyPopUp('Error: pay was not added!')
                $timeout($scope.closePopUp, 4000);
            });
        }

        vm.getHostelPayment = function() {
            return vm.hostels.filter(function (h) {
                return h.Number == vm.studentInfo.Hostel;
            })[0].MonthPaymentSum;
        }
    }
})(angular);