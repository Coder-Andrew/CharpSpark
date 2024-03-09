/*
 * Base class for an object that will automatically handle form submission.  This one
 * is designed for a very typical form that has lots of input elements and a submit button.
 * When the form is submitted, the data is collected from all the input elements and
 * stored into the data property, where each key value is the name="" of the input element.
 * 
 * Derived classes can then override the submitForm() method to handle the form data in any way
 * they like.  Also has the loadForm() method to pre-populate the form with data (so it could be used
 * in an edit form scenario).
 * 
 * I've added form validation via a lightweight manual mechanism I wrote for the JavaScript testing with Jest project.
 * See that code if you need to validate an array of objects.
 */
export function containsIllegalCharacters(input) { return /[\\_\|\^%=+\(\)*\[\]\<\>\~\`]+/.test(input) }

class FormModule {

    /* An internal representation of the data in the form.  This is a "protected" property, so it can be accessed
     * by derived classes, but not by external code.  Keys are the name="" of the input elements in the form.  Values
     * are the current value of the input element or the value to set.
     */
     _data = {};
 
     constructor(formElement, validationSchema) {
         // check if formElement really is a form element.  You could easily use a div or something that contains
         // the form and then just select the form element yourself.
         if (!(formElement instanceof HTMLFormElement)) {
             throw new Error('FormModule must be instantiated with a form element');
         }
         this.formElement = formElement;
         this.formElement.addEventListener('submit', this.submit);  // NOTE: adding the listener to the form not the button
         this.validationSchema = validationSchema ? validationSchema : {};
     }
 
     /* -------------------------------------------------------------------------------
         Methods to override in custom form
        -------------------------------------------------------------------------------
      */
     /* On document ready, here's your chance to populate the form with data. */
     loadFormData() {
         console.log('Override this method to pre-populate the form with data.');
         // When you override, just put values into the _data property and then call setFormData()
         this.setFormData();
     }
 
     /* Code to execute when the form is submitted. Comment this out or customize it if you ever call it.*/
     submitForm() {
         alert(`Form to be submitted with data: ${JSON.stringify(this._data)}`);
         console.log(`Validation schema contains ${Object.keys(this.validationSchema).length} keys`);
     }
 
     /* -------------------------------------------------------------------------------
         Helper methods that all derived classes should be able to use
        -------------------------------------------------------------------------------
     */
 
     // Takes the values in _data and puts them into the form elements, by name
     setFormData() {
         for (const key in this._data) {
             const element = this.formElement.querySelector(`[name="${key}"]`);
             if (element) {
                 // if element is a checkbox, set the checked property
                 if (element.type === 'checkbox' && (this._data[key] === '1' || this._data[key] === 'on')) {
                     element.click();
                 }
                 // You'll have to handle other types of input elements here, such as radio buttons
                 else {
                     // Otherwise, set the value property
                     element.value = this._data[key];
                 }
             }
         }
     }
 
     /* Copy data from the form elements into our internal _data property. */
     pullDataFromForm() {
         this._data = Object.fromEntries(new FormData(this.formElement));
     }
 
     /* Validate the form data against the validation schema. */
     validateFormData() {
         const errors = Object.keys(this.validationSchema)
             .filter(key => !this.validationSchema[key].fn(this._data[key]))
             .map(key => new ValidationError(key, this.validationSchema[key].message));
         window.scrollTo(0, 0);
         return errors;
     }
 
     /* -------------------------------------------------------------------------------
         Callback functions.  These can be added to event listeners.  These need to be
         a function expression with an arrow function to preserve the 'this' context, 
         otherwise you won't have access to the _data property.
        -------------------------------------------------------------------------------
      */
 
     /* This is the submit handler function. It collects all the current form data 
     *  as key-value pairs in the data property and then calls the submitForm() method 
     *  to do something with the data.
     */ 
     submit = (event) => {
         event.preventDefault();
         this.pullDataFromForm();
         this.submitForm();      // This will call the submitForm() method you write in your derived class
     } 
 
 }
 
 /* Just a simple class to hold a validation error message. */
 class ValidationError {
     constructor(name, message) {
         this.name = name;
         this.message = message;
     }
 }
 
 export { FormModule, ValidationError };