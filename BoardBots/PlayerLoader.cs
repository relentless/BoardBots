using BoardBots.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BoardBots {
    public class PlayerLoader {
        private List<string> _validationErrors = new List<string>();

        public List<IPlayer> LoadPlayersFrom(List<Type> types) {

            var playerList = new List<IPlayer>();

            foreach (Type type in types) {

                if (!type.GetInterfaces().Contains(typeof(IPlayer))) {
                    // don't log an error as there may be many non-IPlayer types in the same assembly
                    continue;
                }

                if (!type.IsClass) {
                    _validationErrors.Add(type.FullName + " is not a class");
                    continue;
                }

                if (type.GetConstructor(Type.EmptyTypes) == null) {
                    _validationErrors.Add(type.FullName + " does not have a parameterless constructor");
                    continue;
                }

                try {
                    object createdPlayer = Activator.CreateInstance(type);
                    playerList.Add(createdPlayer as IPlayer);
                }
                catch (Exception ex) {
                    _validationErrors.Add(type.FullName + string.Format(" threw an exception while loading: {0}", ex.InnerException == null ? ex.Message : ex.InnerException.Message));
                }
            }

            return playerList;
        }

        public List<string> ValidationErrors {
            get {
                return _validationErrors;
            }
        }
    }
}
