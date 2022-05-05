<script>
import { ref } from "vue";
import { HTTP } from "../api/http-common";
import DoctorsItem from "../components/DoctorsItem.vue";

export default {
  components: { DoctorsItem },
  data() {
    return {
      date: "",
      doctors: [],
      errors: [],
      newDoctor: 0,
    };
  },
  props: ["id"],

  created() {
    this.init();
  },
  methods: {
    init() {
      this.getDoctors();
    },
    dateChanged() {
      return this.date;
    },
    modalClose() {
      this.newDoctor = 0;
    },
    getDoctors() {
      console.log("reftrests");
      HTTP.get(`/api/doctors`)
        .then((response) => {
          this.doctors = response.data;
          return this.doctors;
        })
        .catch((e) => {
          this.errors.push(e);
        });
    },
    getVisitById() {},
  },
};
</script>

<template>
  <div>
    <div class="details flex justify-center">
      <button
        class="bg-green-500 hover:bg-green-900 px-2 py-2 rounded-md flex text-white"
        @click="this.newDoctor = 1"
      >
        <i class="px-1 self-center"><DocumentationIcon /></i>
        New Doctor
      </button>
    </div>
    <div class="flex flex-col" v-if="!this.$route.params.id">
      <DoctorsItem
        v-for="doctor of this.doctors"
        :key="doctor.id"
        :data="doctor"
      />
    </div>
    <div class="flex flex-col" v-else>
      <span v-for="doctor in this.doctors" :key="doctor.id" class="w-full">
        <DoctorsItem :data="doctor" v-if="doctor.id == this.$route.params.id" />
      </span>
    </div>
    <div
      v-if="this.newDoctor"
      class="fixed z-10 inset-0 overflow-y-auto"
      aria-labelledby="modal-title"
      role="dialog"
      aria-modal="true"
    >
      <div
        class="flex items-end justify-center min-h-screen pt-4 px-4 pb-20 text-center sm:block sm:p-0"
      >
        <div
          class="fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity"
          aria-hidden="true"
        ></div>

        <span
          class="hidden sm:inline-block sm:align-middle sm:h-screen"
          aria-hidden="true"
          >&#8203;</span
        >
        <div
          class="relative inline-block align-bottom bg-white rounded-lg text-left overflow-hidden shadow-xl transform transition-all sm:my-8 sm:align-middle sm:max-w-lg sm:w-full"
        >
          <div class="bg-white px-4 pt-5 pb-4 sm:p-6 sm:pb-4">
            <div class="sm:flex sm:items-start">
              <DoctorsItem
                :data="{}"
                :popup="1"
                @modalClose="this.modalClose()"
                @doctorsRefresh="this.getDoctors()"
              />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
