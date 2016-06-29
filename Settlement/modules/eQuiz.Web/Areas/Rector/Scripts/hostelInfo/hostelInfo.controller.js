(function (angular) {
    angular.module('settlementModule').controller('StudentController', StudentController);

    StudentController.$inject = ['$scope', '$filter', 'hostelDataService', '$routeParams', 'hostelInfo', '$location', '$timeout'];

    function StudentController($scope, $filter, hostelDataService, $routeParams, hostelInfo, $location, $timeout) {
        var vm = this;

        vm.hostelInfo = hostelInfo;
        
        $scope.showNotification = false;
        $scope.showWarning = false;
 
        vm.currentTab = 'Student';
        vm.modelChanged = false; // Indicates whether data in the model was changed

        function activate() {
            hostelDataService.getHostelInfo().then(function (response) {
                vm.hostelInfo = response.data;
                return vm.hostelInfo;
            });
            }

        vm.saveProfile = function () {
            studentDataService.saveHostelProfileInfo(vm.hostelInfo)
            .success(function (res) {
                $scope.showNotifyPopUp('Hostel data was sucessfully saved!')
                vm.modelChanged = false;
                $timeout($scope.closePopUp, 4000);
            })
            .error(function (res) {
                $scope.showNotifyPopUp('Error: hostel data was not saved!')
                $timeout($scope.closePopUp, 4000);
            });
            
        };

        vm.cancelProfile = function () {
            activate();
            vm.modelChanged = false;
        }; // Cancel unsaved changes in the profile

        vm.validationCheck = function () {
            return vm.hostelInfo.Name && vm.hostelInfo.Address && vm.hostelInfo.MonthPaymentSum && vm.modelChanged == true;
        };

    };
})(angular);