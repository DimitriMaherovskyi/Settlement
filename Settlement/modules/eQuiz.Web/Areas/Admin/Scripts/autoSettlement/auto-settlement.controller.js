(function (angular) {
    angular.module('settlementModule').controller('AutoSettlementController', AutoSettlementController);

    AutoSettlementController.$inject = ['$scope', '$filter', 'autoSettlementDataService', 'studentsList'];

    function AutoSettlementController($scope, $filter, autoSettlementDataService, studentsList) {
        var vm = this;

        var orderBy = $filter('orderBy');
        vm.search = ''; // Represents search field on the form
        vm.myPredicate = null;
        vm.tablePage = 0; // Current table page
        vm.resultsPerPage = 10;
        vm.resultsCount = [10, 25, 50, 100]; // Possible numbers of results per page
        vm.studentsWereSettled = false;
        vm.changesWereSaved = false;
        vm.headers = [
    {
        name: 'Name',
        field: 'Name',
        predicateIndex: 0
    }, {
        name: 'Rating',
        field: 'Rating',
        predicateIndex: 1
    }, {
        name: 'Hostel',
        field: 'Hostel',
        predicateIndex: 2
    }, {
        name: 'Room',
        field: 'Room',
        predicateIndex: 3
    },
        ];
        vm.students = [];

        function activate() {
            vm.students = studentsList;
            vm.students.forEach(function (currVal, index, array) {
                currVal.Id = currVal.Id.toString();
                currVal.Name = currVal.Name.toString();
            }); // Converts received data to string values
            generatePredicate();
        };

        activate();

        function generatePredicate() {
            vm.myPredicate = [null, null, null, null];
        }; // Generates empty predicates that are used for ordering

        function clearPredicatesExcept(index) {
            var temp = vm.myPredicate[index];
            generatePredicate();
            vm.myPredicate[index] = temp;
        }; // Clears all predicates except the one with a specified index

        vm.refreshPredicate = function (index) {
            if (vm.myPredicate[index] === null) {
                var item = null;
                switch (index) {
                    case 0:
                        item = '+Name';
                        break;
                    case 1:
                        item = '+Rating';
                        break;
                    case 2:
                        item = '+Hostel';
                        break;
                    case 3:
                        item = '+Room';
                        break;
                }
                vm.myPredicate[index] = item;
            }
            else if (vm.myPredicate[index][0] === '+') {
                vm.myPredicate[index] = '-' + vm.myPredicate[index].slice(1);
            }
            else if (vm.myPredicate[index][0] === '-') {
                vm.myPredicate[index] = null;
            }
            clearPredicatesExcept(index);
        }; // Changes the value of the predicate with specified index and clears all others 

        vm.direction = function (index) {
            if (vm.myPredicate) {
                if (vm.myPredicate[index] === null) {
                    return null;
                };
                if (vm.myPredicate[index][0] === '+') {
                    return true;
                };
                return false;
            };
            return null;
        }; // Gets the order direction of the predicate with specified index

        vm.order = function (predicate, reverse) {
            vm.students = orderBy(vm.students, predicate, reverse);
            vm.predicate = predicate;
        }; // Orders the data based on the specified predicate

        vm.numberOfPages = function () {
            return Math.ceil(vm.searchFiltered.length / vm.resultsPerPage);
        };

        vm.getNumber = function (num) {
            return new Array(num);
        };

        vm.goToPage = function (page) {
            vm.tablePage = page;
        };

        vm.settleStudents = function () {
            autoSettlementDataService.settleStudents().success(function (res) {
                autoSettlementDataService.getSettleResult().then(function (result) {
                    studentList = result.data;
                });
                $scope.showNotifyPopUp('Students were successfully settled!')
                $timeout($scope.closePopUp, 5000);
                vm.studentsWereSettled = true;
            })
            .error(function (res) {
                $scope.showNotifyPopUp('Error: students were not settled!')
                $timeout($scope.closePopUp, 5000);
            });
        }

        vm.saveChanges = function () {
            autoSettlementDataService.saveChanges().success(function (res) {
                $scope.showNotifyPopUp('Changes were sucessfully saved!')
                $timeout($scope.closePopUp, 5000);
                vm.changesWereSaved = true;
            })
            .error(function (res) {
                $scope.showNotifyPopUp('Error: changes were not saved!')
                $timeout($scope.closePopUp, 5000);
            });
        }
        vm.discardChanges() = function () {
            autoSettlementDataService.discardChanges().success(function (res) {
                $scope.showNotifyPopUp('Changes discarded!');
                activate();
                $timeout($scope.closePopUp, 5000);
                vm.studentsWereSettled = false;
            })
            .error(function (res) {
                $scope.showNotifyPopUp('Error: changes was not discarded!')
                $timeout($scope.closePopUp, 5000);
            });
        }
    };

})(angular);