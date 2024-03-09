import {FormModule, containsIllegalCharacters} from './form-module.js';

const validationSchema = {
    'institutionName': {
        fn: x => typeof x === 'string' && x.length > 0 && x.length <= 100 && !containsIllegalCharacters(x),
        message: 'Institution is required and must be 100 characters or less and cannot contain illegal characters'
    },
    'educationSummary': {
        fn: x => typeof x === 'string' && x.length > 0 && x.length <= 250 && !containsIllegalCharacters(x),
        message: 'Education Summary is required and must be 250 characters or less and cannot contain illegal characters'
    },
    'startDate': {
        fn: x => typeof x === 'string' && new Date(x) < new Date.now(),
        message: 'Start Date is required and must be in the past'
    },
    'endDate': {
        fn: x => typeof x === 'string',
        message: 'End Date is required, if not completed enter expected completion date'
    },
    'completed': {
        fn: x => typeof x === 'string' && (x === 'true' || x === 'false'),
        message: 'Completion field is required'
    },
    'degreeType': {
        fn: x => typeof x === 'string' && x.length > 0 && x.length <= 100,
        message: 'Degree Type is required'
    },
    'major': {
        fn: x => typeof x === 'string' && x.length > 0 && x.length <= 50 && !containsIllegalCharacters(x),
        message: 'Major is required'
    },
    'minor': {
        fn: x => typeof x === 'string' && x.length <= 50 && !containsIllegalCharacters(x) || x === null,
        message: 'Minor must be 50 characters or less and cannot contain illegal characters'
    }
}

class EducationModule extends FormModule {
    constructor(formElement) {
        super(formElement, validationSchema);
    }

    loadFormData() {
        return;
    }
    
    submitForm() {
        this.pullDataFromForm();
        const errors = this.validateFormData();
        if (errors.length > 0) {
            console.log('Validation failed');
            console.log(errors);
            return;
        }
        console.log('Validation passed');
        console.log(`Form to be submitted with data: ${JSON.stringify(this._data)}`);
    }

    addEducationToList(educationElement, educationList) {
        const institution = educationElement.querySelector("#institutionName").value;
        const educationSummary = educationElement.querySelector("#educationSummary").value;
        const startDate = educationElement.querySelector("#startDate").value;
        const endDate = educationElement.querySelector("#endDate").value;
        const complete = educationElement.querySelector("#completed").value;
        const degreeType = educationElement.querySelector("#degreeType").value;
        const major = educationElement.querySelector("#major").value;
        const minor = educationElement.querySelector("#minor").value;
    
        if (!institution || !educationSummary || !startDate || !endDate || !complete || !degreeType || !major) return;
        const education = {
            "institution": institution,
            "educationSummary": educationSummary,
            "startDate": startDate,
            "endDate": endDate,
            "complete": complete,
            "degree": [{
                "type": degreeType,
                "major": major,
                "minor": minor
            }]
        }
        educationList.push(education);
    }

    addEducationFromContainer(educationContainer, educationList) {
        educationList = [];
        Array.from(educationContainer.children).forEach(child => {
            addEducationToList(child, educationList);
        });
    }

}