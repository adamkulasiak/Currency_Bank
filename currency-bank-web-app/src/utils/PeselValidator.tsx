export default function validatePesel(pesel: string): boolean {
  if (pesel.length !== 11) {
    return false;
  } else return true;
}
