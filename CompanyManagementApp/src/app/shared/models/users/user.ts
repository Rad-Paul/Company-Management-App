export default class User{
    firstName : string | undefined;
    lastName : string | undefined;
    userName : string | undefined;
    email : string | undefined;
    phoneNumber? : string | undefined;

    constructor(
        firstName: string,
        lastName: string, 
        userName: string,
        email: string,
        phoneNumber?: string
    )
    {
        firstName = firstName;
        lastName = lastName;
        userName = userName;
        email = email,
        phoneNumber = phoneNumber
    }
}