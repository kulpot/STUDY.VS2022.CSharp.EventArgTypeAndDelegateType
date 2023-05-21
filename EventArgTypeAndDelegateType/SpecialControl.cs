using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventArgTypeAndDelegateType
{
    class SpecialControl : Control
    {
        //Keep track of last x pos to contrast with current x pos
        private int _lastXPos;
        // Create an event from the defined event delegate below
        // This is what will be "hooked" by the mainform
        public event MovedLeftEventHandler MoveLeft;

        public SpecialControl()
        {
            // Make it visible
            this.BackColor = Color.Red;
            // Set the lastpos as current pos at startup
            // Avoiding any initial unwanted behavior
            _lastXPos = this.Location.X;
        }

        /// <summary>
        /// Use the Move event in conjunction with our own
        /// </summary>
        protected override void OnMove(EventArgs e)
        {
            base.OnMove(e);

            // If this control has moved left
            if(_lastXPos > this.Location.X)
            {
                int moveAmount = _lastXPos - this.Location.X;
                // Construct event args which are defined below
                MovedLeftEventArgs eventArgs = new MovedLeftEventArgs(moveAmount);
                // Raise event and pass in our event args
                // Event args will be available to any object that has
                // registered to the MoveLeft event
                OnMoveLeft(eventArgs);
            }

            // When were done calculating, save the last x position to contrast
            // with the current position the next time the mouse move event is raised
            _lastXPos = this.Location.X;
        }

        /// <summary>
        /// This is the method defined for the MoveLeft event. Mark this event as private
        /// if you will never override it. Otherwise mark it as protected virtual.
        /// </summary>
        protected virtual void OnMoveLeft(MovedLeftEventArgs e)
        {
            // Check to see if the event is handled anywhere (in simple terms)
            if(MoveLeft != null)
            {
                // This is where you send data to event handlers
                // Send the object related to the event as first argument (in my case the form)
                // Send in e which is the event args passed in from onMouseMove with the moveAmount
                MoveLeft(this, e);
            }
        }
    }

    // Define the delegate here, that can handle the MovedLeftEventArgs
    // You may be able to use a predefined eventArg derived type for what your doing
    // but here we will define our own.
    delegate void MovedLeftEventHandler(object sender, MovedLeftEventArgs e);

    // Define our own event args type. Typically these types expose read only properties
    // and the values are set through the constructor. In this example I only need the move amount
    // but you require a class that can hold more data which is perfectly fine.
    class MovedLeftEventArgs : EventArgs
    {
        private int _moveAmount;

        public MovedLeftEventArgs(int moveAmount)
        {
            _moveAmount = moveAmount;
        }

        public int MoveAmount
        {
            get { return _moveAmount; }
        }
    }
}
