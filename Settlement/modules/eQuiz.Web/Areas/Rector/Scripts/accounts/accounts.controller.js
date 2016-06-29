(function (angular) {
    angular.module('settlementModule').controller('AccountsController', AccountsController);

    AccountsController.$inject = ['$scope', '$filter', 'accountsDataService', 'accountsList', 'accountRoles', '$timeout'];

    function AccountsController($scope, $filter, accountsDataService, accountsList, accountRoles, $timeout) {
        var vm = this;
        var orderBy = $filter('orderBy');
        vm.search = ''; // Represents search field on the form
        vm.myPredicate = null;
        vm.tablePage = 0; // Current table page
        vm.resultsPerPage = 10;
        vm.resultsCount = [10, 25, 50, 100]; // Possible numbers of results per page
        vm.selectedGroup = [];
        vm.userId;
        vm.newAccount = {};
        vm.newAccountBoxOpened = false;
        vm.changeAccountBoxOpened = false;
        vm.accounts = accountsList;
        vm.roles = accountRoles;
        vm.changedAccount = {};

        vm.headers = [
    {
        name: 'UserName',
        field: 'UserName',
        predicateIndex: 0
    }, {
        name: 'Email',
        field: 'Email',
        predicateIndex: 1
    }, {
        name: 'Institute',
        field: 'Institute',
        predicateIndex: 2
    }, {
        name: 'CreatedDate',
        field: 'CreatedDate',
        predicateIndex: 3
    }, {
        name: 'LastLoginDate',
        field: 'LastLoginDate',
        predicateIndex: 4
    }, {
        name: 'Role',
        field: 'Role',
        predicateIndex: 5
    }, {
        name: 'Quote',
        field: 'Quote',
        predicateIndex: 6
    }, {
        name: 'FirstName',
        field: 'FirstName',
        predicateIndex: 7
    }, {
        name: 'LastName',
        field: 'LastName',
        predicateIndex: 8
},];

        function activate() {
                accountsDataService.getAccounts().then(function (respond) {
                  vm.accounts = respond.data;
                 })
            generatePredicate();
        };
        generatePredicate();

        function generatePredicate() {
            vm.myPredicate = [null, null, null, null, null, null, null, null, null];
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
                        item = '+UserName';
                        break;
                    case 1:
                        item = '+Email';
                        break;
                    case 2:
                        item = '+Institute';
                        break;
                    case 3:
                        item = '+CreatedDate';
                        break;
                    case 4:
                        item = '+LastLoginDate';
                        break;
                    case 5:
                        item = '+Role';
                        break;
                    case 6:
                        item = '+Quote';
                        break;
                    case 7:
                        item = '+FirstName';
                        break;
                    case 8:
                        item = '+LastName';
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
            vm.accounts = orderBy(vm.accounts, predicate, reverse);
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

        vm.toggleChangeAccount = function (id) {
            console.log(id);
            vm.changeAccountBoxOpened = true;
            vm.temp = vm.accounts.filter(function (a) {
                return a.UserId == id;
            })[0];
            angular.copy(vm.temp, vm.changedAccount);
            vm.chosenRole = vm.roles.filter(function (v) {
                return v.RoleId === vm.changedAccount.RoleId;
            })[0];
        };

        vm.toggleAddAccount = function (id) {
            vm.addAccountBoxOpened = !vm.addAccountBoxOpened;
        };

        vm.saveAccount = function () {
            vm.changedAccount.RoleId = vm.chosenRole.RoleId;
            accountsDataService.changeAccount(vm.changedAccount).success(function (res) {
                activate();
                vm.changeAccountBoxOpened = false;
                $scope.showNotifyPopUp('Account was successfully saved!')
                $timeout($scope.closePopUp, 4000);
            })
            .error(function (res) {
                $scope.showNotifyPopUp('Error: account was not saved!')
                $timeout($scope.closePopUp, 4000);
                vm.changeAccountBoxOpened = false;
            });;
        }

        vm.addAccount = function () {
            vm.newAccount.RoleId = vm.chosenRole.RoleId;
            accountsDataService.addAccount(vm.newAccount).success(function (res) {
                activate();
                vm.newAccountBoxOpened = false;
                $scope.showNotifyPopUp('New account was successfully added!')
                $timeout($scope.closePopUp, 4000);
            })
            .error(function (res) {
                $scope.showNotifyPopUp('Error: new account was not added!')
                $timeout($scope.closePopUp, 4000);
                vm.newAccountBoxOpened = false;
            });; 
        }

        vm.deleteAccount = function (userId) {
            accountsDataService.deleteAccount(userId).success(function (res) {
                activate();
                $scope.showNotifyPopUp('Account was successfully deleted!')
                $timeout($scope.closePopUp, 4000);
            })
            .error(function (res) {
                $scope.showNotifyPopUp('Error: account was not deleted!')
                $timeout($scope.closePopUp, 4000);
            });;
        }

        vm.getRoleName = function (roleId) {
            return vm.roles.filter(function (v) {
                return v.RoleId === roleId;
            })[0].RoleName;
        }

        vm.validateNewAccountForm = function() {
            if (vm.newAccount.UserName && vm.newAccount.Email && vm.newAccount.Institute && vm.chosenRole && vm.newAccount.Quote && vm.newAccount.FirstName && vm.newAccount.LastName && vm.newAccount.Password != undefined) {
                return true;
            }
            else
            {
                return false;
            }
        }
        vm.validateChangeAccountForm = function () {
            if(vm.changedAccount.Username && vm.changedAccount.Email && vm.changedAccount.Institute && vm.chosenRole && vm.changedAccount.Quote && vm.changedAccount.FirstName && vm.changedAccount.LastName != undefined)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    };

})(angular);