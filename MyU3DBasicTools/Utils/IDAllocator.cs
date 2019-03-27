using System.Collections.Generic;

namespace SimpleAI.Utils
{
    public sealed class IDAllocator
    {
        private static readonly IDAllocator TheInstance = new IDAllocator();

        static IDAllocator() { }

        private IDAllocator() { }

        ~IDAllocator()
        {
            Reset();
        }

        public static IDAllocator Instance
        {
            get
            {
                return TheInstance;
            }
        }

        /// <summary>
        /// The ids list to hold the ids recycled for future reusing .
        /// </summary>
        private List<int> RecycledIDs = new List<int>();

        private int RecycledIDCount = 0;

        private static int NextNewValidID = 0;

        private int TheInvalidID = -1;

        public int InvalidID
        {
            get
            {
                return TheInvalidID;
            }
        }

        public int FreedIDCount
        {
            get
            {
                return RecycledIDCount;
            }
        }

        /// <summary>
        /// Try to retrive a valid unique id from recycled ids
        /// if there is one, or return a invalid id.
        /// </summary>
        /// <returns>The id got.</returns>
        private int RetriveRecycledID()
        {
            int id = TheInvalidID;

            if (RecycledIDCount > 0)
            {
                id = RecycledIDs[0];
                RecycledIDs.RemoveAt(0);
            }

            return id;
        }

        /// <summary>
        /// Get a unique id valid to use.
        /// </summary>
        /// <returns>The unique id.</returns>
        public int GetID()
        {
            int id = RetriveRecycledID();

            if (id == TheInvalidID)
            {
                id = ++NextNewValidID;
            }
            
            return id;
        }

        /// <summary>
        /// Recycle a id not in use at present for future using.
        /// </summary>
        /// <param name="id">The id the recycle.</param>
        public void RecycleID(int id)
        {
            if (id != TheInvalidID)
            {
                RecycledIDCount++;
                RecycledIDs.Add(id);
            }
        }

        /// <summary>
        /// Reset the properties to init status.
        /// Clear the containers.
        /// </summary>
        public void Reset()
        {
            RecycledIDCount = 0;
            RecycledIDs.Clear();
        }
    }
}
