export class LoginResponseModel {
    constructor(
        public token: string,
        public name: string | null) { }
}
