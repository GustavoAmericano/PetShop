using System;
using System.Collections.Generic;
using System.Linq;
using PetShop.Core.ApplicationService;
using PetShop.Core.Entities;

namespace PetShop
{
    public class MainMenu
    {
        private readonly PetUI.PetUI _petUI;
        private readonly OwnerUI.OwnerUI _ownerUI;
        public MainMenu(IPetService petService, IOwnerService ownerService)
        {
            _petUI = new PetUI.PetUI(petService, ownerService);
            _ownerUI = new OwnerUI.OwnerUI(ownerService, petService);
            ShowUI();
        }

        private void ShowUI()
        {
            bool _isRunning = true;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("\u001b[32m1.\u001b[0m Pets menu\r\n" +
                                  "\u001b[32m2.\u001b[0m Owners menu\r\n" +
                                  "\r\n\r\nType \u001b[32mindex\u001b[0m of the action" +
                                  "\r\nType \u001b[31mexit\u001b[0m to exit the program.\r\n");
                Console.Write("Input: ");
                var input = Console.ReadLine();
                switch (input.ToLower())
                {
                    case "1":
                        _petUI.ShowMenu();
                        break;
                    case "2":
                        _ownerUI.ShowMenu();
                        break;
                    case "exit":
                        return;
                }

            }
        }
    }
}