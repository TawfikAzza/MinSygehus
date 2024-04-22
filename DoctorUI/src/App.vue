<template>
  <div>
    <header>
      <nav>
        <RouterLink to="/create">Create Patient</RouterLink>
        <RouterLink to="/delete">Delete Patient</RouterLink>
        <RouterLink to="/measurements">View Measurements</RouterLink>
      </nav>
    </header>
    <main>
      <RouterView />
    </main>
  </div>
</template>

<style scoped>
header {
  background-color: #333;
  color: white;
  padding: 1rem;
}

nav {
  display: flex;
  gap: 1rem;
}

main {
  margin-top: 20px; /* Adjust this value as needed */
}

body {
  display: flex;
  flex-direction: column;
}
</style>
