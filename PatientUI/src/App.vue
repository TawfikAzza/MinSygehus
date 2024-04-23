<script setup lang="ts">
import {ref} from 'vue';

const ssn = ref<string>(null);
const systolic = ref<number>(null);
const diastolic = ref<number>(null);

const isLoggedIn = ref<boolean>(false);

const login = async () => {
  if (!ssn.value) {
    console.error('Missing SSN!');
    return;
  }
  else {
    const request = {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
    };
    let requestUrl = 'http://localhost:8081/Patient/?ssn=' + ssn.value;
    const response = await fetch(requestUrl, request);
    const result = await response.json();
    //Check if request code is 200
    if (response.status !== 200) {
      console.error('Invalid SSN!');
      return;
    }
    console.log(result);
    isLoggedIn.value = true;
  }
};

const sendMeasurement = async () => {
  if (!ssn.value
    || !systolic.value
      || !diastolic.value) {
    console.error('Missing required fields!');
    return;
  }

  const postMeasurementDto = {
    Ssn: ssn.value,
    Systolic: systolic.value,
    Diastolic: diastolic.value,
  };

  const request = {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(postMeasurementDto),
  };
  let requestUrl = 'http://localhost:9091/Measurement';

  //Get the result from the server
  const response = await fetch(requestUrl, request);
  const result = await response.json();

  console.log(result);
};

</script>

<template>
  <main>
    <!-- Login (Get SSN) -->
    <section v-if="!isLoggedIn" class="section-container">
      <h1>Enter your SSN</h1>
      <input v-model="ssn" type="text" placeholder="SSN">
      <button @click="login">Login</button>
    </section>
    <!-- Record measurement -->
    <section v-if="isLoggedIn" class="section-container">
      <h1>Record your measurement</h1>
      <input v-model="systolic" type="number" placeholder="Systolic">
      <input v-model="diastolic" type="number" placeholder="Diastolic">
      <button @click="sendMeasurement">Send</button>
      <button @click="isLoggedIn = false">Logout</button>
    </section>
  </main>
</template>

<style scoped>

.section-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  height: 100%;
  width: 100%;
}
</style>
