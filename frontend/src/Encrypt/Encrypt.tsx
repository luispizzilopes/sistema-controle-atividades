import * as CryptoJS from 'crypto-js';

const privateKey: string = 'Jh4L8pRfGq2sA9wXzY3vN6mBc1o7Ku0tEi5xUdP';

const encryptText = (inputText: string): string => {
  const encrypted: string = CryptoJS.AES.encrypt(inputText, privateKey).toString();
  return encrypted.replace("/", "&");
};

const decryptText = (encryptedText: string): string => {
  encryptedText = encryptedText.replace("&", "/");
  const decrypted: string = CryptoJS.AES.decrypt(encryptedText, privateKey).toString(CryptoJS.enc.Utf8);
  return decrypted;
};

export { encryptText, decryptText };
