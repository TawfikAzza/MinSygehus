<script setup>
import {ref} from "vue";

const ssn = ref('');

const isDeletePatientDisabled = ref(false);

const deletePatient = async () => {
  console.log("Deleting patient");
  console.log(ssn.value);

  const request = {
    method: 'DELETE',
    headers: {
      'Content-Type': 'application/json',
    },
  };
  let requestUrl = 'http://localhost:8081/Patient/?ssn=' + ssn.value;

  //Get the result from the server
  const response = await fetch(requestUrl, request);
  if (response.status !== 200) {
    if (response.status === 418) {
      isDeletePatientDisabled.value = true;
      return;
    }
    isDeletePatientDisabled.value = false;

    console.error(response);
  }
  const result = await response.json();
  console.log(result);
}
</script>

<template>
  <section>
    <label class="error" v-if="isDeletePatientDisabled">Deletion of patients is temporarily disabled!</label>
    <label for="ssn">SSN:</label>
    <input v-model="ssn" type="text" id="ssn" name="ssn" required>
    <button @click="deletePatient">Delete</button>
  </section>
</template>

<style scoped>
section {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.error {
  color: red;
}
</style>
